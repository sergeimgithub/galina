﻿//. can't include this file into pigs.cshtml file - need to find a way
function RenderImageFolder(div, data) {
    var len = data.Lines.length;
    var result = "";
    for (var i = 0; i < len; i++)
    {
            // /images20210703/Fox/DSC04113.JPG
            var line = data.Lines[i];
            result += '<table style="display:inline-block">';
            result += '<tr><td>';
            result += '<span style="font-size:7px">' + line + '</span>';
            result += '</td></tr>';
            result += '<tr><td>';
            result += '<img src="' + line + '" style="height:300px;" />';
            result += '</td></tr>';
            result += '</table>';
            result += '&nbsp;';
        }
    div.innerHTML = result;
}
