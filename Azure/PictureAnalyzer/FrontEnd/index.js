var app = new Vue({
    el: '#app',
    data: {
        message: 'Hello Vue.js!',
        imgFile: null,
        tags: 'no tags yet',
        imgUrl: null
    },
    methods: {
        uploadToBlob: function (url) {
            $.ajax({
                url: url,
                type: "PUT",
                headers: {
                    'Content-Type': 'application/octet-stream',
                    'x-ms-version': '2017-04-17',
                    'x-ms-blob-type': 'BlockBlob',
                    'x-ms-blob-content-type': 'image/jpg'
                },
                data: this.imgFile,
                cache: false,
                contentType: false,
                processData: false,
                success: function () { console.warn('uploaded file to blob') },
                error: function () { console.warn('failed to upload file') }
            })
        },
        sendPicture: function () {
            $.ajax({
                url: "https://kltraining.azurewebsites.net/api/GetSasToken/"+this.imgFile.name,
                type: "GET",
                cache: false,
                contentType: false,
                processData: false,
            }).done(result => this.uploadToBlob(result))                
        },
        fileChanged(e) {            
            const files = e.target.files || e.dataTransfer.files
            this.imgFile = files.length ? files[0] : null
        }
    }
});

const apiBaseUrl = "https://kltraining.azurewebsites.net";
const connection = new signalR.HubConnectionBuilder()
      .withUrl(`${apiBaseUrl}/api`)
      .configureLogging(signalR.LogLevel.Information)
      .build();
      connection.on('analysisReceived', (message) => newMessage(message));
      connection.onclose(() => console.log('disconnected'));
      connection.start()      
      .catch(console.error);

      function newMessage(message) {
          app.tags = message.tags;
          app.imgUrl = message.url;
       console.warn('message logged' + message);
       console.warn(this.tags);
      }