$(document).on("click", '#btnPlaceOrder', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    console.log(aHref);
    $.ajax(
        {
            type: "Post",
            url: aHref,
            data: {
              
            },
            statusCode: {
                200: function (data) {
                    alert('1');
                    AfterSavedAll();
                },
                201: function (data) {
                    $("#tdbodyid").html(data);
                    document.getElementById("closerModal").click();
                },
                400: function (data) {
                    $(".box_modal").html(data.responseText);
                },
                404: function (data) {
                    alert('3');
                    bootbox.alert('<span style="color:Red;">Error While Saving Outage Entry Please Check</span>', function () { });
                }
                //}, success: function () {
                //    alert('4');

            },
            //success: function (data, textStatus) {
            //    console.log(textStatus.);




            //    $("#tdbodyid").html(data);




            //},
            //error: function (err) {
            //    $(".box_modal").html(err.responseText);

            //}

        });

})