const { DataTypes } = require("sequelize");
const { sequelize } = require("../config/db");

const Leave = sequelize.define(
  "Leave",
  {
    id: {
      type: DataTypes.INTEGER,
      primaryKey: true,
      autoIncrement: true
    },

    leaveType: {
      type: DataTypes.STRING(),
      allowNull: false
    },

    fromDate: {
      type: DataTypes.DATE,
      allowNull: false
    },

    toDate: {
      type: DataTypes.DATE,
      allowNull: false
    },

    reason: {
      type: DataTypes.TEXT,
      allowNull: false
    },

    status: {
      type: DataTypes.ENUM(
        "Pending",
        "Approved",
        "Rejected"
      ),
      defaultValue: "Pending"
    },

    employeeId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },

    managerId: {
      type: DataTypes.INTEGER,
      allowNull: false
    }
  },
  {
    tableName: "Leaves",
    timestamps: true
  }
);

module.exports = Leave;
