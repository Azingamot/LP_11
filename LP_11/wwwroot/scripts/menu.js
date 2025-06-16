function toggleSideBar()
{
    var toggleButton = document.getElementById('toggle-button');
    var sideBar = document.getElementById('sidebar');

    sideBar.classList.toggle('close');
    toggleButton.classList.toggle('rotate');
}