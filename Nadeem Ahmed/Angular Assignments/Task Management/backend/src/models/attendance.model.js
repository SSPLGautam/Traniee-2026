const { DataTypes } = require("sequelize");
const { sequelize } = require("../config/db");

const Attendance = sequelize.define("Attendance",
    {
        id: {
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
        },


        attendanceDate: {
            type: DataTypes.DATEONLY,
            allowNull: false,

        },

        attendanceTime: {
            type: DataTypes.DATE,
            allowNull: true,

        },

        status:{
            type: DataTypes.ENUM(
                "Present",
                "Absent",
                "Leave"
            ),
            allowNull: false
        },

        employeeId:{
            type:DataTypes.INTEGER,
            allowNull:false
        }

    });

module.exports = Attendance;