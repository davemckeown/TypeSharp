/// <reference path="node.d.ts" />
var count = 0;
function HelloWorld() {
    console.log("Hello World : " + count);
    count++;
}
process.stdin.resume();
process.stdin.setEncoding("utf8");
process.stdin.on("data", function (input) {
    eval(input);
});
process.on("uncaughtException", function (err) {
    process.stderr.write(err.toString());
});
//@ sourceMappingURL=NodeBroker.js.map
