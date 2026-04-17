let currentStep = 0;
let steps = document.querySelectorAll(".step");
let circles = document.querySelectorAll(".circle");

function show(index) 
{
    steps.forEach(step => step.style.display = "none");
    steps[index].style.display = "block";

    circles.forEach(c => c.classList.remove("active"));
    for (let i = 0; i <= index; i++)
    {
        circles[i].classList.add("active");
    }
}
show(currentStep);
document.querySelectorAll(".next").forEach(btn => 
{
    btn.addEventListener("click", () => 
    {
        if (currentStep < steps.length - 1) 
        {
            currentStep++;
            show(currentStep);
        }
    });
});

document.querySelectorAll(".prev").forEach(btn =>
{
    btn.addEventListener("click", () => 
    {
        if (currentStep > 0) 
        {
            currentStep--;
            show(currentStep);
        }
    });
});

document.getElementById("Form").addEventListener("submit", function(e)
{
    e.preventDefault();
    let formData = {};
    document.querySelectorAll("input, select").forEach(el =>
    {
        if (el.id) formData[el.id] = el.value;
    });
    let allData = JSON.parse(localStorage.getItem("formData")) || [];
    allData.push(formData);
    localStorage.setItem("formData", JSON.stringify(allData));
    document.getElementById("success").style.display = "block";

    console.log(allData);
});