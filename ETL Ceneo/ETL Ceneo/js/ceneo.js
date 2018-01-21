function getItemId()
{
    var itemId = document.getElementById("itemId").value;
    console.log(itemId);

    WebService.StartETL(itemId, function (result) {
        startETL_result(result),
            onfailurestartSEPA;
    },
        function (error) { onfailurestartETL_2(error), null });

  
}


function startETL_result(result) {

    //if (result == "000")
    showConfirmButton();
    alert(result);

    //do something if we get a result

}
function onfailurestartETL() {
    //do something if webmethod crashed
    //alert("Proccess not started.\nPlease verify input parameters!");
    showConfirmButton();
    alert(result);

}
function onfailurestartETL_2(error) {
    //do something if webmethod not reached
    showConfirmButton();
    alert("Proccess not started.\nPlease try again or contant us");

}