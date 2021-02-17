function topNavBar() {
  var x = document.getElementById("myLinks");
  if (x.style.display === "block") {
    x.style.display = "none";
  } else {
    x.style.display = "block";
  }
}

(function(){
$(".burgerMenu").on("click", function(ev) {
  ev.preventDefault();
  $(this).toggleClass("animateBurger");
  $(".mainNav").slideToggle("fast");
});
$(window).on("resize", function(ev) {

  //console.info(window.innerWidth);
  if (window.innerWidth > 720) {
    $("nav ul").attr("style", "");
  }
});
function navHighlight(elem, home, active) {
    var url = location.href.split('/'),
        loc = url[url.length -1],
        link = document.querySelectorAll(elem);
    for (var i = 0; i < link.length; i++) {
        var path = link[i].href.split('/'),
            page = path[path.length -1];
        if (page == loc || page == home && loc == '') {
            link[i].parentNode.className += ' ' + active;
            document.body.className += ' ' + page.substr(0, page.lastIndexOf('.'));
            }
        }
    }
    
navHighlight('.mainNav ul li a', 'index.html', 'current'); 
})();


//on click of learner "ready" button: grant permission to view camera and screen





//grant camera and mic permissions.
const permissionsNames = [
    "camera",
    "microphone",
]

const getAllPermissions = async () => {
    const allPermissions = []
    // We use Promise.all to wait until all the permission queries are resolved
    await Promise.all(
        permissionsNames.map(async permissionName => {
            try {
                let permission
                permission = await navigator.permissions.query({ name: permissionName })
                console.log(permission)
                allPermissions.push({ permissionName, state: permission.state })
            }
            catch (e) {
                allPermissions.push({ permissionName, state: 'error', errorMessage: e.toString() })
            }
        })
    )
    return allPermissions
}







// notes overlay
function notesOn() {
  document.getElementById("overlay").style.display = "block";
}

function notesOff() {
  document.getElementById("overlay").style.display = "none";
}



//Invigilator Page Code to redirect and pass data
function startViewing() {
    //passing myLIst[] array values into session ID's
    '<%Session["myLIst1"] = myList[0]%>';
    '<%Session["myLIst2"] = myList[1]%>';
    '<%Session["myLIst3"] = myList[2]%>';
    '<%Session["myLIst4"] = myList[3]%>';

    //load Exam page
    location.replace("Exam.cshtml")
    


}

function buttonClicked(id) {

    $(function () {
        $('#load').on('click', function () {
            $('#activeLearners').load('/LoggedIn/Invigilator?handler=LearnerPartial');
        });
    });


}

function firstLoad() {
    $('#activeLearners').load('/LoggedIn/Invigilator?handler=LearnerPartial');
}


function moveLearner(id) {
    //$.post('/LoggedIn/Invigilator?handler=MoveLearner', {id : id})
    //needs finishing to pass data to the handler function.

    $.ajax({
        traditional: true,
        type: "POST",
        url: '/LoggedIn/Invigilator?handler=MoveLearner',
        data: id,
        success: ok,
        dataType: "json"
    });
}