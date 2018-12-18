const app=require('express')();
const server=require('http').createServer(app);
const io=require('socket.io')(server);

server.listen(3000,()=>{
    console.log('Socket IO server listening on port 3000');
});

const EVENT_UNITY="UNITY";
const EVENT_UNITY_NON_MNIST="UNITY_NON_MNIST";
const EVENT_TENSORFLOW="TENSORFLOW";
const EVENT_TENSORFLOW_NON_MNIST="TENSORFLOW_NON_MNIST";


io.on('connection',function(socket){

    console.log("새로운 클라이언트 : ",socket.id);

    socket.on('disconnect',(socket)=> {
        console.log('클라이언트 종료 : ',socket.id);
    });

    socket.on(EVENT_UNITY,(data)=>{
        data=JSON.parse(data);
        console.log('unity에서 이미지 base64 수신 완료');

        io.emit(EVENT_TENSORFLOW,data);

    });


    socket.on(EVENT_TENSORFLOW,(data)=> {
        console.log("prediction from tensorflow : ",data);
        io.emit(EVENT_UNITY,data);
    });

    socket.on(EVENT_TENSORFLOW_NON_MNIST,()=> {
        console.log("This is not MNIST Image");
        io.emit(EVENT_UNITY_NON_MNIST);
    });
});
