// Write your JavaScript code.

function callAjax(url, data) {
    
    var fullUrl = getDomainName() + "/" + url;

    //console.log(fullUrl);

    return $.ajax({
        type: 'POST',
        async: true,
        //data: JSON.stringify(data),
        contentType: 'application/json',
        url: fullUrl
    });
}

function getDomainName(){
    return "http://localhost:5000";
}