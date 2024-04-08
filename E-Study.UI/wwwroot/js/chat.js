class Message {
    constructor(username, text) {
        this.userName = username;
        this.text = text;
        //this.when = when;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
//const whenInput = document.getElementById('when');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    //when.innerHTML =
    //    (currentdate.getMonth() + 1) + "/"
    //    + currentdate.getDate() + "/"
    //    + currentdate.getFullYear() + " "
    //    + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
   // let when = new Date();
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let listItem = document.createElement('li');
    listItem.className = isCurrentUserMessage ? "chat-message chat-message-right" : "chat-message";

    let messageWrapper = document.createElement('div');
    messageWrapper.className = "d-flex overflow-hidden";

    let sender = document.createElement('p');
    sender.innerHTML = message.userName;

    let messageTextWrapper = document.createElement('div');
    messageTextWrapper.className = "chat-message-wrapper flex-grow-1";

    let messageText = document.createElement('div');
    messageText.className = "chat-message-text";
    messageText.innerHTML = "<p class='mb-0'>" + message.text + "</p>";

    let messageTime = document.createElement('div');
    messageTime.className = "text-end text-muted mt-1";
    messageTime.innerHTML = "<i class='bx bx-check-double text-success'></i><small>" + message.dateTime + "</small>";

    let avatarWrapper = document.createElement('div');
    avatarWrapper.className = "user-avatar flex-shrink-0 ms-3";

    let avatar = document.createElement('div');
    avatar.className = "avatar avatar-sm";
    let avatarImg = document.createElement('img');
    avatarImg.src = "../../assets/img/avatars/user.png";
    avatarImg.alt = "Avatar";
    avatarImg.className = "rounded-circle";

    avatar.appendChild(avatarImg);
    avatarWrapper.appendChild(avatar);

    messageTextWrapper.appendChild(messageText);
    messageTextWrapper.appendChild(messageTime);

    messageWrapper.appendChild(sender);
    messageWrapper.appendChild(messageTextWrapper);
    messageWrapper.appendChild(avatarWrapper);

    listItem.appendChild(messageWrapper);

    chat.appendChild(listItem);
}
