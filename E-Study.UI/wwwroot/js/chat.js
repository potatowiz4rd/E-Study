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
const messagesQueue = [];

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
    let listItem = document.createElement('li');
    listItem.className = "chat-message chat-message-right";

    let messageWrapper = document.createElement('div');
    messageWrapper.className = "d-flex overflow-hidden";

    let messageTextWrapper = document.createElement('div');
    messageTextWrapper.className = "chat-message-wrapper flex-grow-1";

    let messageText = document.createElement('div');
    messageText.className = "chat-message-text";
    messageText.innerHTML = "<p class='mb-0'>" + message.text + "</p>";

    messageTextWrapper.appendChild(messageText);

    messageWrapper.appendChild(messageTextWrapper);

    listItem.appendChild(messageWrapper);

    let chatHistory = document.querySelector('.chat-history');
    chatHistory.appendChild(listItem);
}
