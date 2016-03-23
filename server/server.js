var express = require('express');

var app = express();
var requestId = 0;

var recusive = function(i, end, res) {
    
    console.log('send ' + i + ' to : ' + res.requestId);
    res.write('hello ' + i + '\r\n');
    
    if (i >= end) {
        res.end();
        return;
    }
    
    if (res.stop) {
        console.log('request was aborted!');
        return;
    }
    
    setTimeout(() => {
        recusive(++i, end, res);
    }, 200);
};

app.get('/stream', function(req, res) {
    req.connection.on('close',function(){    
       res.stop = true;
    });
    
    ++requestId;
    res.requestId = requestId;
    recusive(0, 30, res);
});

app.listen(3000, function() {
    console.log('\033[2J');
    console.log('application started!');
});