/* Author: Jeremy Jones

*/






















//this function includes all necessary js files for the application
function include(file) {

    var script = document.createElement('script');
    script.src = file;
    script.type = 'text/javascript';
    script.defer = true;

    document.getElementsByTagName('head').item(0).appendChild(script);

}

/* include any js files here */
include('js/libs/jquery-1.7.1.js');
include('js/libs/modernizr-2.0.6.min.js');
include('js/libs/prefixfree.min.js');