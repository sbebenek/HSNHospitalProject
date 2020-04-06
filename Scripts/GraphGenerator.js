console.log("Graph Generator script has been successfully linked to this page.");


/**
 * Generates the activity record graph at the given html element id, with the given 1-10 rating value of each weekday. Also is able to set the width and height of the canvas (in px) in the last two parameters.
 * @param int canvasId The id of the canvas element the graph will be printed to
 * @param int sundayVal
 * @param int mondayVal
 * @param int tuesdayVal
 * @param int wednesdayVal
 * @param int thursdayVal
 * @param int fridayVal
 * @param int saturdayVal
 * @param int width
 * @param int height
 */
function generateGraph(canvasId, sundayVal, mondayVal, tuesdayVal, wednesdayVal, thursdayVal, fridayVal, saturdayVal, width, height) {

    //Change these values to the width and height of the canvas
    //TODO: try to take these values as method paramaters
    const MAX_WIDTH = width;
    const MAX_HEIGHT = height;

    var c = document.getElementById(canvasId);
    var ctx = c.getContext("2d");

    ctx.canvas.width = MAX_WIDTH;
    ctx.canvas.height = MAX_HEIGHT;

    var currentDay = new Date().getDay(); //will be a number from 0-6, with 0 being sunday

    var weekdayValues = [sundayVal, mondayVal, tuesdayVal, wednesdayVal, thursdayVal, fridayVal, saturdayVal];

    var barWidth = MAX_WIDTH / 16;
    var barGap = MAX_WIDTH / 20;
    var barBottom = MAX_HEIGHT - MAX_HEIGHT / 6;
    var barMaxHeight = MAX_HEIGHT / 8;
    var barStartPos = MAX_WIDTH / 8;
    var todayBarColor = "#EF8326";
    var barColor = "#F2AC6F";
    var textStartXPos = barStartPos + barWidth / 6;
    var textYPos = barBottom + MAX_HEIGHT * 0.1;

    console.log("(from generateGraph js method) The graph has begun drawing");
    
    //line position will be calculated based on max width and height
    ctx.moveTo(0 + MAX_WIDTH / 12, barBottom);
    ctx.lineTo(MAX_WIDTH - MAX_WIDTH / 12, barBottom);
    ctx.stroke();
    ctx.fillStyle = barColor;
    ctx.font = "16px Helvetica";
    console.log("Mapping the bar height: " + weekdayValues[0].map(0, 10, barMaxHeight, barBottom));
    console.log("Bar Bottom: " + barBottom);
    console.log("Bar Max height: " + barMaxHeight);


    //(top left  x coord, top left y coord, width, height)
    for (var i = 0; i < weekdayValues.length; i++) {
        //if the bar being drawn is the one for today
        if (i == currentDay) {
            //set it to today's color
            ctx.fillStyle = todayBarColor;
        }
        else {
            ctx.fillStyle = barColor;
        }
        ctx.fillRect(barStartPos, barBottom, barWidth, weekdayValues[i].map(0, 10, 0, barBottom - barMaxHeight) * -1);
        barStartPos = barStartPos + barWidth + barGap;
    }

    //drawing the text character for each weekday
    ctx.fillStyle = "#000000";
    ctx.fillText("S", textStartXPos, textYPos);
    textStartXPos = textStartXPos + barWidth + barGap;
    ctx.fillText("M", textStartXPos, textYPos);
    textStartXPos = textStartXPos + barWidth + barGap;
    ctx.fillText("T", textStartXPos, textYPos);
    textStartXPos = textStartXPos + barWidth + barGap;
    ctx.fillText("W", textStartXPos, textYPos);
    textStartXPos = textStartXPos + barWidth + barGap;
    ctx.fillText("T", textStartXPos, textYPos);
    textStartXPos = textStartXPos + barWidth + barGap;
    ctx.fillText("F", textStartXPos, textYPos);
    textStartXPos = textStartXPos + barWidth + barGap;
    ctx.fillText("S", textStartXPos, textYPos);

}

/**
 * Maps one number range to another - reference https://gist.github.com/xposedbones/75ebaef3c10060a3ee3b246166caab56
 */
Number.prototype.map = function (in_min, in_max, out_min, out_max) {
    return (this - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}