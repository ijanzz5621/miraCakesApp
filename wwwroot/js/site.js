// Write your JavaScript code.

function callAjax(url, method, data) {
    
    var fullUrl = getDomainName() + "/" + url;

    //console.log(fullUrl);

    return $.ajax({
        type: method,
        async: true,
        data: JSON.stringify(data),
        contentType: 'application/json',
        url: fullUrl
    });
}

function getDomainName(){
    return "http://localhost:5000";
}