﻿var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chat')  // Use the hub route, not an action route
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('sendMessage', message);
}
