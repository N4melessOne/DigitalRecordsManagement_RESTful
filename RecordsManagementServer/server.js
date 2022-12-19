'use strict';
var http = require('http');
var port = process.env.PORT || 8889; //the PHP REST is listening on the 8888 port
//so the server will listen on 8889.

http.createServer(function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/plain' });
    res.end('Hello World\n');
}).listen(port);