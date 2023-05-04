let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);

connection.on("ChatReset", function () {
    let chatBoxMessage = document.querySelector('.chatbox__messages')
    chatBoxMessage.innerHTML = '';
});

connection.on("ReceiveMessage", function (message) {

    let container = document.querySelector('.chatbox');
    let messageHtml =
        `<div class="messages__item messages__item--operator">
            <h6>${message.fromName}: </h6>
            ${message.text}
         </div>
        `;

    let element = document.createElement('div');

    element.innerHTML = messageHtml;

    let chatBoxMessage = document.querySelector('.chatbox__messages')
    chatBoxMessage.prepend(element);


    let chatboxSupport = document.querySelector('.chatBoxMessage');
    chatboxSupport.appendChild(form);

    container.appendChild(chatboxSupport);
});

connection.start().then(function () {
    $(".btn-light").prop('disabled', false);
}).catch(function (err) {
    return console.error(err.toString());
});

document.querySelector(".btn-light").addEventListener("click", function (event) {
    console.log(connection)
    let message = document.getElementById('message-input');
    console.log(message)
    connection.invoke("SendMessage", message.value).catch(e => console.log(e))

    event.preventDefault();
    message.value = '';

});