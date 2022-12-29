'use strict';
var http = require('http');
var port = process.env.PORT || 8889; 

//server code for authentication

http.createServer(function (req, res) {
    var requestMethod = req.method;
    switch (requestMethod) {
        case 'POST': null;
        case 'PUT': null;
        default: null;
    }
}).listen(port);