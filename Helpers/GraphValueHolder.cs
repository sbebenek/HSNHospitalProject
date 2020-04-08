using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSNHospitalProject.Helpers
{
    /// <summary>
    /// Sam Bebenek - This class holds all the information needed to pass to the GraphGenerator.js method (The ID of the canvas element to print to, and 
    /// the average rating value for each day of the week).
    /// </summary>
    public class GraphValueHolder
    {

        /// <summary>
        /// The ID of the HTML element to print the graph to
        /// </summary>
        public string canvasId { get; set; } = "none";
        /// <summary>
        /// The average rating value for Sunday
        /// </summary>
        public int sundayVal { get; set; } = 1;

        /// <summary>
        /// The average rating value for Monday
        /// </summary>
        public int mondayVal { get; set; } = 1;
        /// <summary>
        /// The average rating value for Tuesday
        /// </summary>
        public int tuesdayVal { get; set; } = 1;
        /// <summary>
        /// The average rating value for Wednesday
        /// </summary>
        public int wednesdayVal { get; set; } = 1;
        /// <summary>
        /// The average rating value for Thursday
        /// </summary>
        public int thursdayVal { get; set; } = 1;
        /// <summary>
        /// The average rating value for Friday
        /// </summary>
        public int fridayVal { get; set; } = 1;
        /// <summary>
        /// The average rating value for Saturday
        /// </summary>
        public int saturdayVal { get; set; } = 1;

        //CONSTRUCTOR
        /// <summary>
        /// Sam Bebenek - This class holds all the information needed to pass to the GraphGenerator.js method (The ID of the canvas element to print to, and 
        /// the average rating value for each day of the week).
        /// </summary>
        /// <param name="divId"></param>
        /// <param name="sundayVal"></param>
        /// <param name="mondayVal"></param>
        /// <param name="tuesdayVal"></param>
        /// <param name="wednesdayVal"></param>
        /// <param name="thursdayVal"></param>
        /// <param name="fridayVal"></param>
        /// <param name="saturdayVal"></param>
        public GraphValueHolder (string canvasId, int sundayVal, int mondayVal, int tuesdayVal, int wednesdayVal, int thursdayVal, int fridayVal, int saturdayVal)
        {
            this.canvasId = canvasId;
            this.sundayVal = sundayVal;
            this.mondayVal = mondayVal;
            this.tuesdayVal = tuesdayVal;
            this.wednesdayVal = wednesdayVal;
            this.thursdayVal = thursdayVal;
            this.fridayVal = fridayVal;
            this.sundayVal = sundayVal;
        }
        
        //override toString() wasn't working so I have to use a new method to basically do the same thing
        /// <summary>
        /// Basically the same thing as a custom toString()
        /// </summary>
        /// <returns></returns>
        public string printValues()
        {
            string returnString = "GraphValueHolder - cavasId = " + this.canvasId + ", sundayVal = " + this.sundayVal + ", mondayVal = " + this.mondayVal
                + ", tuesdayVal = " + this.tuesdayVal + ", wednesdayVal = " + this.wednesdayVal + ", thursdayVal = " + this.thursdayVal 
                + ", fridayVal = " + this.fridayVal + ", saturdayVal = " + this.saturdayVal;
            return returnString;
        }
    }
}