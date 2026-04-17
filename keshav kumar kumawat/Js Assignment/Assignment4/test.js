$(document).ready(function () {
    let active;

    $(document).on("click", ".title, .description", function () {
        active = $(this);

        $("#icon").detach().insertAfter(this).show();
    });

    $("#icon i").click(function () {
        if (!active) return;

        if ($(this).hasClass("fa-bold")) active.toggleClass("fa-bold");
        if ($(this).hasClass("fa-italic")) active.toggleClass("fa-italic");
        if ($(this).hasClass("fa-underline")) active.toggleClass("fa-underline");
        if ($(this).hasClass("fa-text-slash")) active.removeClass("fa-text-slash");
    });

    $(document).on("click", ".question-image", function () {
        $(this).siblings(".question-image-picker").click();
    });

    $(document).on("click", ".option-image, .option-image-icon, .option-row img", function () {
        const $picker = $(this).siblings("input[type=file]");
        if ($picker.length) {
            $picker.click();
        }
    });

    $(document).on("change", ".question-type", function () {
        let box = $(this).closest(".question-card");
        handleType(box, $(this).val());
    });

    function handleType(box, type) {
        let $list = box.find(".option-list");
        let $addOptionRow = box.find(".add-option-row");
        console.log("Add option: ", $addOptionRow);



        $list.empty();

        if (type === "mul-choice") {
            $addOptionRow.show();
            $list.append(getOptionRow("radio", 1));
        }
        else if (type === "check") {
            $addOptionRow.show();
            $list.append(getOptionRow("checkbox", 1));
        }
        else if (type === "drop") {
            $addOptionRow.show();
            $list.append(`<select><option>Option 1</option></select>`);
        }
        else if (type === "para") {
            $addOptionRow.hide();
            $list.append(`<textarea placeholder="Long answer"></textarea>`);
        }
        else if (type === "short") {
            $addOptionRow.hide();
            $list.append(`<input type="text" placeholder="Short answer">`);
        }
        else if (type === "date") {
            $addOptionRow.hide();
            $list.append(`<input type="date" disabled>`);
        }
    }

    function getOptionRow(type, count) {
        return `
    <div class="option-row" style="display:flex; gap:10px; margin-top:10px;">
        <input type="${type}" disabled>
        <input type="text" class="option-input" placeholder="Option ${count}">
        <i class="fa-solid fa-xmark option-delete-icon" style="cursor:pointer;"></i>
    </div>`;
    }

    $(document).on("change", ".question-type", function () {
        let box = $(this).closest(".question-card");
        handleType(box, $(this).val());
    });

    $(document).on("click", ".option-delete-icon", function (e) {
        let $list = $(this).closest(".option-list");

        if ($list.find(".option-row").length >= 1) {
            $(this).closest(".option-row").remove();
        }
    });

    $(document).on("click", ".copy-question", function () {
        const $question = $(this).closest('.question-card');
        const $clone = $question.clone(false);

        $clone.find("#icon").remove();


        $clone.insertAfter($question);
    });
    $(document).on("click", ".copy-tag", function () {
        const $tagCopy = $(this).closest('.copy-delete-tag');
        const $clone = $tagCopy.clone(true);
        $clone.insertBefore($tagCopy);
    });

    $(document).on("click", ".delete-question", function () {
        const $question = $(this).closest('.question-card');
        if ($('.question-card').length >= 1) {
            $question.remove();
        }
    });

    $(document).on("click", ".delete-tag", function () {
        const $tagDelete = $(this).closest('.copy-delete-tag');
        if ($('.copy-delete-tag').length >= 1) {
            $tagDelete.remove();
        }
    });

    $(document).on("change", ".question-image-picker, .option-image-picker", function () {
        const file = this.files[0];
        const $picker = $(this);
        if (!file) return;

        const reader = new FileReader();
        reader.onload = function (e) {
            const $target = $picker.siblings("i.option-image-icon, img, i.option-image, i.question-image").first();
            if ($target.is('img')) {
                $target.attr('src', e.target.result);
            } else {
                $target.replaceWith('<img src="' + e.target.result + '" style="cursor: pointer; max-width: 100px; max-height: 100px;">');
            }
        };
        reader.readAsDataURL(file);
    });

    $(document).on("click", ".addOptionText", function (e) {
        e.stopPropagation();

        let $list = $(this).closest(".question-card").find(".option-list");
        let count = $list.find(".option-row").length + 1;

        const newOption = $(`
        <div class="option-row" style="display:flex; align-items:center; gap:10px; margin-top:10px;">
            <input type="radio" class="option-radio" disabled>
            <input type="text" class="option-input" placeholder="Option ${count}">

            <div class="option-image" style="margin-top:5px;">
                <i class="fa-solid fa-image option-image-icon" style="cursor:pointer;"></i>
                <input type="file" class="option-image-picker" accept="image/*" style="display:none;">
                <i class="fa-solid fa-xmark option-delete-icon" style="cursor:pointer; color:rgb(81, 0, 255);"></i>
            </div>
        </div>
    `);

        $list.append(newOption);
    });
    $(document).on("keydown", ".option-input", function (e) {
        if (e.key === "Enter") {
            e.preventDefault();
            $(this).closest(".question-card").find(".addOptionText").click();
        }
    });

    $("#first-Option, label[for='first-Option']").click(function () {
        $("#option1ImageDiv").show();
    });

    $(".fa-plus").click(() => {
        const $firstCard = $(".question-card").first();

        if (!$firstCard.is(':visible')) {
            $firstCard.show();
        } else {
            const $clone = $firstCard.clone(true);

            $clone.find("input[type='text']").val("");
            $clone.find(".option-input").val("");
            $clone.find("input[type='file']").val("");
            $clone.insertAfter($(".question-card").last());
        }
    });

    let x = 0;

    $(".icon-T").click(function () {
        x++;

        const $firstCard = $(".copy-delete-tag").first();
        if (!$firstCard.is(":visible")) {
            $firstCard.show();
        } else {
            let $clone = $firstCard.clone(true);
            $clone.find("input[type='text']").val("");
            $clone.show().insertAfter($(".copy-delete-tag").last());
        }
    });
});

$("#addform").on("click", function () {

    let formData = [];
    let output = "";
    let title = $(".title").val();
    let desc = $(".description").val();
    console.log(title, desc)
    if (title || desc) {
        output += `
<div style="background:#fff; padding:20px; margin:10px 0; border-top:5px solid purple; border-radius:10px;">
    
    <h2>${title}</h2>

    <p>${desc}</p>


</div>
`;
    }




    $(".question-card").each(function () {

        let q = $(this).find(".ques input").val();
        let type = $(this).find("select").val();

        let options = [];

        $(this).find(".option-input").each(function () {
            let opt = $(this).val();
            if (opt.trim() !== "") {
                options.push(opt);
            }
        });

        if (q && q.trim() !== "") {
            formData.push({
                question: q,
                type: type,
                options: options
            });
        }

    });

    $(".copy-delete-tag").each(function () {

        let heading = $(this).find("input").val();
        let disc = $(this).find(".description").val();

        if (heading || disc) {
            output += `
<div style="background:#fff; padding:20px; margin:10px 0; border-top:5px solid purple; border-radius:10px;">
    
    <h2>${heading}</h2>

    <p>${disc}</p>


</div>
`;
        }


    });


    formData.forEach(function (item) {

        output += `<div style="background:#fff; padding:10px; margin:10px 0;">`;
        output += `<p><b>${item.question}</b></p>`;

        if (item.type === "mul-choice") {
            item.options.forEach(opt => {
                output += `<label><input type="radio"> ${opt}</label><br>`;
            });
        }

        else if (item.type === "check") {
            item.options.forEach(opt => {
                output += `<label><input type="checkbox"> ${opt}</label><br>`;
            });
        }

        else if (item.type === "drop") {
            output += `<select>`;
            item.options.forEach(opt => {
                output += `<option>${opt}</option>`;
            });
            output += `</select>`;
        }

        else if (item.type === "short") {
            output += `<input type="text"><br>`;
        }

        else if (item.type === "para") {
            output += `<textarea></textarea>`;
        }

        else if (item.type === "date") {
            output += `<input type="date">`;
        }

        output += `</div>`;
    });

    $("#samePage").remove();
    $(".container").after(`<div id="samePage">${output}</div>`);
});
