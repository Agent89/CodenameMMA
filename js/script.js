/* Author: Jeremy Jones

*/

function getUserName(UserID,fieldID) {
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
                $("#"+fieldID).text(r.d);
            }
        });
}