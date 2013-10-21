/// <reference path="node.d.ts" />

var count = 0;

function HelloWorld() {
    console.log("Hello World : " + count);
    count++;
}

process.stdin.resume();

process.stdin.setEncoding("utf8");

process.stdin.on("data", input => {
    eval(input);
});

process.on("uncaughtException", err => {
    process.stderr.write(err.toString());
});