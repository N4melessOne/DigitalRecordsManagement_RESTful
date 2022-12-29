'use strict';
var http = require("http");
var express = require('express');
var app = express();
var phpUri = 'localhost:8888/serverIndex.php';

var server = app.listen(8889, "127.0.0.1", function () {
    var host = server.address().address
    var port = server.address().port
})


app.use(express.json());

app.get('/server', function (req, res) {

});


/*
app.get('/', function (response, request) {
    var obj = JSON.parse(request.body);
    if (obj.id !== null) {
        var action = { "action": "get" };
        obj.push(action);

        var phpServerPost = {
            uri: phpUri,
            body: JSON.parse(obj),
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        }

        postRequest(phpServerPost, function (postResponse) {
            var objResponse = JSON.parse(postResponse);
            if (objResponse.error === null) {
                response = postResponse;
            }
            else {
                response = {
                    error: objResponse.error,
                    message: objResponse.message
                    }
            }
        });
    }
    else {

    }

});

app.post('', function (response, request) {

});

app.put('', function (response, request) {

});

app.delete('', function (response, request) {

});*/





/*http.createServer(function (req, res) {
    var requestMethod = req.method;
    switch (requestMethod) {
        case 'GET': null;
        case 'POST': null;
        case 'PUT': null;
        case 'DELETE': null;
        default: null;
    }
}).listen(port);*/