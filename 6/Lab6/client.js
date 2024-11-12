const net = require('net')

const client = new net.Socket();
client.connect(3000, '127.0.0.1', () => {
    console.log('Connected to server');
    sendNumber();
});

function sendNumber() {
    const number = Math.floor(Math.random() * 100);
    console.log(`Sending number: ${number}`);
    client.write(number.toString());
}

client.on('data', (data) => {
    console.log(`Received from server: ${data.toString()}`);
});

client.on('close', () => {
    console.log('Connection closed');
});

setInterval(sendNumber, 5000);