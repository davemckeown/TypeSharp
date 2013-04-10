// Ambient definiton of the define function
declare var define: any;





define(function () {
    var a = function () { }; //constructor
    a.prototype = {
        method1: function () {

        },
        method2: function () {

        }
    };//body   

    return a;
});
