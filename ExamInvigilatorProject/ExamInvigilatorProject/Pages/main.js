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













