let tasks = JSON.parse(localStorage.getItem("tasks")) || [];

$(document).ready(function () {


    tasks.forEach(function (task) {
        addTaskToUI(task);
    });


    $("#addBtn").click(function () {

        let taskText = $("#taskInput").val().trim();

        if (taskText === "") {
            alert("Enter a task first!");
            return;
        }


        let duplicate = some(function (t) {
            return t.text.toLowerCase() === taskText.toLowerCase();
        });

        if (duplicate) {
            alert("This task already exists!");
            return;
        }

        let newTask = {
            id: tasks.length,
            text: taskText,
            completed: false
        };

        tasks.push(newTask);
        localStorage.setItem("tasks", JSON.stringify(tasks));

        addTaskToUI(newTask);

        $("#taskInput").val("");
    });


    $("#taskList").on("click", ".delete-btn", function () {

        let row = $(this).closest(".task-row");
        let id = Number(row.attr("data-id"));

        tasks = tasks.filter(function (t) {
            return t.id !== id;
        });

        localStorage.setItem("tasks", JSON.stringify(tasks));

        row.remove();
    });


    $("#taskList").on("change", "input[type='checkbox']", function () {

        let row = $(this).closest(".task-row");
        let id = Number(row.attr("data-id"));
        let inputBox = row.find(".box");

        tasks.forEach(function (t) {
            if (t.id === id) {
                t.completed = !t.completed;
            }
        });

        localStorage.setItem("tasks", JSON.stringify(tasks));

        if ($(this).is(":checked")) {
            inputBox.css("text-decoration", "line-through");
        } else {
            inputBox.css("text-decoration", "none");
        }
    });

});


function addTaskToUI(task) {
    console.log(task)

    let newRow = `
    <div class="task-row" data-id="${task.id}" style="display:flex; margin-top:10px; align-items:center;">
        
        <div style="margin-left:30px;">
            <input type="checkbox" ${task.completed ? "checked" : ""}>
        </div>

        <div style="width:39%;">
            <input type="text" class="form-control box" value="${task.text}"
            style="${task.completed ? 'text-decoration: line-through;' : ''}">
        </div>

        <div style="margin-left:340px; cursor:pointer;" class="delete-btn">
            <i class="fa-solid fa-delete-left"></i>
        </div>

    </div>
    `;

    $("#taskList").append(newRow);
}