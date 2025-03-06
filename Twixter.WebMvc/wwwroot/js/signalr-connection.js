// SignalR ile bağlantı kuruyoruz
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/postHub")  // SignalR Hub'ının URL'si
    .build();

// SignalR ile yeni bir post geldiğinde tetiklenen fonksiyon
connection.on("ReceiveNewPost", function (post) {
    const newPostHtml = `
            <div class="card border-0 border-bottom p-3" id="post-${post.id}">
                <div class="d-flex">
                    <img src="${post.userProfilePictureUrl}" class="rounded-circle me-3" width="50" height="50" alt="Profile Picture">
                    <div class="flex-grow-1">
                        <div class="d-flex justify-content-between">
                            <div>
                                <strong>${post.userName}</strong> 
                                <span class="text-muted">${post.createdDate}</span>
                            </div>
                        </div>
                        <p class="mt-2 mb-1">${post.content}</p>
                        <div class="d-flex justify-content-between text-muted">
                            <button class="btn btn-sm text-secondary"><i class="bi bi-chat"></i> Comment</button>
                            <button class="btn btn-sm text-secondary"><i class="bi bi-arrow-repeat"></i> Retwix</button>
                            <button class="btn btn-sm text-secondary"><i class="bi bi-heart"></i> Like</button>
                            <button class="btn btn-sm text-secondary"><i class="bi bi-share"></i> Share</button>
                        </div>
                    </div>
                </div>
            </div>`;

    // Yeni gönderiyi sayfaya ekliyoruz
    $(".col-md-6 .p-3").after(newPostHtml);
});

// SignalR bağlantısını başlatıyoruz
connection.start().catch(function (err) {
    return console.error(err.toString());
});

// Form submit event handler
$("#newPostForm").on("submit", function (e) {
    e.preventDefault(); // Formun normal submit işlemini engelliyoruz

    var content = $("#newPostContent").val(); // Textarea'dan içeriği alıyoruz

    // AJAX ile post gönderiyoruz
    $.ajax({
        url: '@Url.Action("CreatePost", "Home")', // Controller ve action URL'si
        type: 'POST',
        data: {
            Content: content // Yeni post verisi
        },
        success: function(response) {
            // Başarılı olursa SignalR üzerinden tüm istemcilere yeni gönderiyi ilet
            connection.invoke("SendNewPost", response);

            // Textarea'yı temizle
            $("#newPostContent").val("");
        },
        error: function() {
            alert("Something went wrong!");
        }
    });
});