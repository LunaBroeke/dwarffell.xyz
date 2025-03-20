// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function updateClock(item) {
	const clock = document.getElementById(item);
	fetch('/api/time')
		.then(response => response.json())
		.then(data => {
			clock.textContent = `Server Time: ${data.time}`;
		})
		.catch(error => {
			console.error("Error fetching Server time:", error);
		})
}
function updateTimer(item, targetTime, refresh) {
    const clock = document.getElementById(item);
    fetch(`/api/time/until?targetTime=${encodeURIComponent(targetTime)}`)
        .then(response => response.json())
        .then(data => {
            if (data.timeUntil) {
                clock.textContent = data.timeUntil;
            } else {
                clock.textContent = data.message;
                if (data.message === "Time is in the past" && refresh === true) {
                    location.reload();
                }
            }
        })
        .catch(error => {
            console.error("Error fetching Server time:", error);
        })
}
function updateEventTime(item, date) {
    const eventTimeElement = document.getElementById(item);
    const eventTime = date;
    const localTime = eventTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', timeZoneName: 'short' });
    eventTimeElement.textContent = localTime;
}
