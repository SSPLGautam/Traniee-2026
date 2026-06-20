const leaveRepository = require("../repositories/leave.repository")

const createLeave =
  async (data) => {

    return await leaveRepository
      .createLeave(data);

  };

const getManagerLeaves =
  async(managerId) => {
  return await leaveRepository.getManagerLeaves(
    managerId
  );
}

const updateLeaveStatus = 
  async(id, status) => {
    return await leaveRepository.updateLeaveStatus(
      id, 
      status
    )
  }

const getAllLeaves =
  async () => {

    return await leaveRepository
      .getAllLeaves();

  };

const getMyLeaves =
  async (userId) => {

    return await leaveRepository
      .getMyLeaves(userId);

  };



module.exports = {
  createLeave,
  getAllLeaves,
  getManagerLeaves,
  getMyLeaves,
  updateLeaveStatus
};