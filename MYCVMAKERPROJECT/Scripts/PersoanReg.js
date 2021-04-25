// Header
var reg  = document.getElementById('reg');
var det  = document.getElementById('det');
var exp  = document.getElementById('exp');
var proj  = document.getElementById('proj');

function activeshadowreg(){
	det.classList.remove("active");
	exp.classList.remove("active");
	proj.classList.remove("active");
	reg.classList.add("active");
}function activeshadowdet(){
	proj.classList.remove("active");
	exp.classList.remove("active");
	reg.classList.remove("active");
	det.classList.add("active");
}
function activeshadowexp(){
	det.classList.remove("active");
	proj.classList.remove("active");
	reg.classList.remove("active");
	exp.classList.add("active");
}
function activeshadowproj(){
	det.classList.remove("active");
	exp.classList.remove("active");
	reg.classList.remove("active");
	proj.classList.add("active");
}

// det
// ----- On render -----
$(function() {

	$('#profile').addClass('dragging').removeClass('dragging');
  });
  
  $('#profile').on('dragover', function() {
	$('#profile').addClass('dragging')
  }).on('dragleave', function() {
	$('#profile').removeClass('dragging')
  }).on('drop', function(e) {
	$('#profile').removeClass('dragging hasImage');
  
	if (e.originalEvent) {
	  var file = e.originalEvent.dataTransfer.files[0];
	  console.log(file);
  
	  var reader = new FileReader();
  
	  //attach event handlers here...
  
	  reader.readAsDataURL(file);
	  reader.onload = function(e) {
		console.log(reader.result);
		$('#profile').css('background-image', 'url(' + reader.result + ')').addClass('hasImage');
  
	  }
  
	}
  })
  $('#profile').on('click', function(e) {
	console.log('clicked')
	$('#mediaFile').click();
  });
  window.addEventListener("dragover", function(e) {
	e = e || event;
	e.preventDefault();
  }, false);
  window.addEventListener("drop", function(e) {
	e = e || event;
	e.preventDefault();
  }, false);
  $('#mediaFile').change(function(e) {
  
	var input = e.target;
	if (input.files && input.files[0]) {
	  var file = input.files[0];
  
	  var reader = new FileReader();
  
	  reader.readAsDataURL(file);
	  reader.onload = function(e) {
		console.log(reader.result);
		$('#profile').css('background-image', 'url(' + reader.result + ')').addClass('hasImage');
	  }
	}
  })

  function addNewExperienceForm(){
	const Form = document.getElementById("button");
	const form = document.getElementById("ExperienceForm");
	const itm = document.getElementById("Experience").outerHTML;
	var z = document.createElement('div');
	z.classList.add("content");
	z.innerHTML =itm;
	form.insertBefore(z, form.childNodes[2]);
  }
  function addNewServicesForm(){
	const Form = document.getElementById("button2");
	const form = document.getElementById("ServicesForm");
	const itm = document.getElementById("Services").outerHTML;
	var z = document.createElement('div');
	z.classList.add("content");
	z.innerHTML =itm;
	form.insertBefore(z, form.childNodes[2]);
  }
  function addNewSkillsForm(){
	const Form = document.getElementById("button");
	const form = document.getElementById("SkillsForm");
	const itm = document.getElementById("Skills").outerHTML;
	var z = document.createElement('div');
	z.classList.add("content");
	z.innerHTML =itm;
	form.insertBefore(z, form.childNodes[2]);
  }
  function addNewProjectsForm(){
	const Form = document.getElementById("button4");
	const form = document.getElementById("ProjectsForm");
	const itm = document.getElementById("Projects").outerHTML;
	var z = document.createElement('div');
	z.classList.add("content");
	z.innerHTML =itm;
	form.insertBefore(z, form.childNodes[2]);
  }