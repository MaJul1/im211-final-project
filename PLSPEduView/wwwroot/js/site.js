// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function sortTable(columnIndex) {
    var table = document.querySelector(".table tbody");
    var rows = Array.from(table.rows);
    var isAscending = table.getAttribute("data-sort-order") !== "desc"; // Default to ascending
    rows.sort(function (rowA, rowB) {
        var cellA = rowA.cells[columnIndex].innerText;
        var cellB = rowB.cells[columnIndex].innerText;
        return isAscending ? cellA.localeCompare(cellB) : cellB.localeCompare(cellA);
    });
    table.innerHTML = "";
    rows.forEach(function (row) {
        table.appendChild(row);
    });
    table.setAttribute("data-sort-order", isAscending ? "desc" : "asc");
}

function sortTableByNumber(columnIndex) {
    var table = document.querySelector(".table tbody");
    var rows = Array.from(table.rows);
    var isAscending = table.getAttribute("data-sort-order") !== "desc"; // Default to ascending

    rows.sort(function (rowA, rowB) {
        var cellA = parseFloat(rowA.cells[columnIndex].innerText) || 0;
        var cellB = parseFloat(rowB.cells[columnIndex].innerText) || 0;
        return isAscending ? cellA - cellB : cellB - cellA;
    });

    table.innerHTML = "";
    rows.forEach(function (row) {
        table.appendChild(row);
    });

    table.setAttribute("data-sort-order", isAscending ? "desc" : "asc");
}