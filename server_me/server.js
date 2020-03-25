var express = require('express')
var http = require('http')
var socketIO = require('socket.io')

var app = express()
var server = http.Server(app)
server.listen(5000)
var io = socketIO(server)

var numberComputer = Math.floor(Math.random() * 101);

io.on('connection',function(socket){
    console.log('Client Connected')

    socket.on('msg.send',function(data){
        console.log(data)
    var playerData = JSON.stringify(data);
    var Data = JSON.parse(playerData);
    var namePlayer = Data.name;
    var numberPlayer = Data.number;
    if(numberPlayer == numberComputer )
    {
        
        console.log('corrent number');
        socket.emit('userWin');
        socket.broadcast.emit('haveWin');
        console.log('have winner in server');
        
    }else if (numberPlayer > numberComputer)
    {
        
        console.log('morethan number');
        socket.emit('moreThan');
    }else if(numberPlayer < numberComputer)
    {
        
        console.log('lessthan number');
        socket.emit('lessThan');
    }
    socket.on('resetnumber', function () {
        newLuckeyNumber = Math.floor(Math.random() * 101);
        numberComputer = newLuckeyNumber;
        console.log('number has reset');
    });
    
    });
   
});

console.log('server started')
    

