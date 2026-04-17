// let handleFAQList=(element) => {

//     var allAnswers = document.getElementsByClassName('answer');

//     for (let i = 0; i < allAnswers.length; i++) {
//         allAnswers[i].style = "display:none";
//         var question = allAnswers[i].previousElementSibling;
//         var icon = question.getElementsByClassName('icon')[0];
//         icon.classList.remove('rotate')
//     }

//     var currentAnswer = element.nextElementSibling;
//     console.log(currentAnswer);
    
//     if(currentAnswer.style.display == "block"){
//         console.log(currentAnswer.style);
//         currentAnswer.style.display = "none";
//     }
//     else{
//         currentAnswer.style.display = "block";
//     }
    
//     var question = currentAnswer.previousElementSibling;
//     var icon = question.getElementsByClassName('icon')[0];
//     icon.classList.add('rotate')

// }

$(document).ready(function(){
  $(".question").click(function(){

    $(this).next(".answer").slideToggle();
     $(this).find(".icon").toggleClass("rotate");

  });
});