var page = require("webpage").create();
var system = require('system');
var fs = require('fs');

if (system.args.length > 1) {
	address = "https://www.youtube.com/results?search_query=" + system.args[1].toLowerCase().replace(" ", "+");
}
else {
	address = "https://www.youtube.com/results?search_query=Monstercat";
}

outputCount = 0;
searchLoc = 0;

page.viewportSize = {
	width: 480,
	height: 800
};
//page.settings.userAgent = "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 5 Build/LMY48B; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/43.0.2357.65 Mobile Safari/537.36";
page.open(address, function(status) {
	if (status !== "success") {
		console.log("ERROR");
		phantom.exit();
	}
	else {
		//console.log(address);
		checkSearchForFirstResult();
	}
});

function checkSearchForFirstResult() {

	var newYoutubeCheck = page.evaluate(function() {
		return document.getElementsByClassName("style-scope ytd-two-column-search-results-renderer").length;
	});

	//If not using new youtube
	var info;

	if (newYoutubeCheck === 0) {

		info = page.evaluate(function() {
			return document.getElementsByClassName("section-list")[0].children[1].children[0].children[0].children[0].children[0].children[0].children[0].href;
		});

		if (info) {
			console.log(info);
		}
		else {
			info = page.evaluate(function() {
				return document.getElementsByClassName("section-list")[0].children[1].children[0].children[2].children[0].children[0].children[0].children[0].href;
			});

			if (info) {
				console.log(info);
			}
		}
	}
	else {
		info = page.evaluate(function() {
			return document.getElementsByClassName("style-scope ytd-two-column-search-results-renderer")[1].children[0].children[1].children[0].children[2].children[0].children[0].children[0].children[0].href;
		});

		if (info) {
			console.log(info);
		}
		else {
			info = page.evaluate(function() {
				return document.getElementsByClassName("style-scope ytd-two-column-search-results-renderer")[1].children[0].children[1].children[0].children[2].children[2].children[0].children[0].children[0].href;
			});
			if (info) {
				console.log(info);
			}
		}
	}
	phantom.exit();
}

function writeCurrPageToFile() {
	fs.write(outputCount + "output.html", page.evaluate(function() {
		return document.body.innerHTML;
	}), 'w');
	page.render(outputCount + "output.png", "png");
	outputCount++;
}
