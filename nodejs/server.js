var port = process.env.PORT || 1347;
var io = require('socket.io')(port);
var shortid = require('shortid');

console.log("server is running on port "+port);
io.on('connection',function(socket){

  var
})
