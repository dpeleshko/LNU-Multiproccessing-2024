const net = require('net')

const server = net.createServer((socket) => {
    console.log('Client connected');

    socket.on('data', (data) => {
        const clientNum = parseInt(data.toString());
        console.log(`Received number from client: ${clientNum}`);

        const serverNum = Math.floor(Math.random() * 100);
        console.log(`Server number: ${serverNum}`);

        let response;
        if (serverNum > clientNum) {
            response = serverNum.toString();
        } else {
            response = clientNum.toString();
        }

        socket.write(response);
    });

    socket.on('close', () => {
        console.log('Client disconnected');
    });
});

server.listen(3000, () => {
    console.log('Server listening on port 3000');
});