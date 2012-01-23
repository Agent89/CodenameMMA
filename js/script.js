/* Author: Jeremy Jones

*/

//Custom Objects
//function User() {
//    this.UserID = 0;
//    this.UserName = "";
//    this.Wallet = new Wallet(0,0);

//}

//function Wallet(amount,refills) {
//    this.Amount = amount;
//    this.Refills = refills;
//}



//Winservice Calls
function getUserInfo(username, password) {
    var params = new Object();
    params.username = username;
    params.password = password;
    var User = new Object();
    $.ajax(
        {
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            url: "MMACode.asmx/GetUserInfo",
            data: $.toJSON(params),
            success: function (r) {
                User = jQuery.parseJSON(r.d);
            },
            error: function (r) {
                alert(r.responseText);
            },
            async: false
        });

    return User;
}

function getUserName(UserID, fieldID) {
    var params = new Object();
    params.UserID = UserID;
    $.ajax(
        {
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            url: "MMACode.asmx/GetUserName",
            data: $.toJSON(params),
            success: function (r) {
                $("#" + fieldID).text(r.d);
            }
        });
}

function getWalletAmount(UserID, fieldID) {
    var params = new Object();
    params.UserID = UserID;

    $.ajax(
        {
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: $.toJSON(params),
            url: "MMACode.asmx/GetWalletAmount",
            success: function (r) {
                $("#" + fieldID).text(r.d);
            }
        });
}

//Javascript functions
function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function loadjscssfile(filename, filetype) {
    var fileref;
    if (filetype == "js") { //if filename is a external JavaScript file
        fileref = document.createElement('script')
        fileref.setAttribute("type", "text/javascript")
        fileref.setAttribute("src", filename)
    }
    else if (filetype == "css") { //if filename is an external CSS file
        fileref = document.createElement("link")
        fileref.setAttribute("rel", "stylesheet")
        fileref.setAttribute("type", "text/css")
        fileref.setAttribute("href", filename)
    }
    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref)
}


//Element Manipulation
function AttachTo(eleID, toID) {
    var toAttachTo = document.getElementById(toID);
    var ele = document.getElementById(eleID);
    //ele.style.position = "absolute";
    ele.style.left = findPosX(toAttachTo) + "px";
    ele.style.top = (findPosY(toAttachTo) + toAttachTo.clientHeight) + "px";
}

function findPosX(obj) {
    var left = 0;
    if (obj.offsetParent) {
        while (1) {
            left += obj.offsetLeft;
            if (!obj.offsetParent)
                break;
            obj = obj.offsetParent;
        }
    }
    else if (obj.x) {
        left += obj.x;
    }
    return left;
}

function findPosY(obj) {
    var top = 0;
    if (obj.offsetParent) {
        while (1) {
            top += obj.offsetTop;
            if (!obj.offsetParent)
                break;
            obj = obj.offsetParent;
        }
    }
    else if (obj.y) {
        top += obj.y;
    }
    return top;
}