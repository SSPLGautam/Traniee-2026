const { Leave, User } = require("../models");

const createLeave = async (data) => {

  return await Leave.create(data);

};



const getMyLeaves = async (employeeId) => {

  return await Leave.findAll({
    where: {
      employeeId
    }
  });

};

const getAllLeaves =
  async () => {

    return await Leave.findAll({

      include: [

        {
          model: User,
          as: "Employee",

          attributes: [
            "id",
            "username",
            "email"
          ]
        },

        {
          model: User,
          as: "Manager",

          attributes: [
            "id",
            "username",
            "email"
          ]
        }

      ]

    });

  };
const getManagerLeaves =
  async (managerId) => {

    return await Leave.findAll({

      where: {
        managerId
      },

      include: [

        {
          model: User,
          as: "Employee",

          attributes: [
            "id",
            "username",
            "email"
          ]
        }

      ]

    });

  };
const updateLeaveStatus =
  async (
    id,
    status
  ) => {

    const leave =
      await Leave.findByPk(id);

    if (!leave) {

      throw new Error(
        "Leave not found"
      );

    }

    leave.status = status;

    await leave.save();

    return leave;

  };

module.exports = {
  createLeave,
  getMyLeaves,
  getManagerLeaves,
  getAllLeaves,
  updateLeaveStatus
};