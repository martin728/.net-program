using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            string message = GetMessageForModel(userId, description);
            if (message != null)
            {
                model.AddAttribute("action_result", message);
                return false;
            }

            return true;
        }

        private string GetMessageForModel(int userId, string description)
        {
            var task = new UserTask(description);

            try
            {
                _taskService.AddTaskForUser(userId, task);
            }
            catch (InvalidUserIdException e)
            {
                Console.WriteLine(e);
                return "Invalid userId";
            }
            catch (UserNotFoundException e)
            {
                Console.WriteLine(e);
                return "User not found";
            }
            catch (TaskAlreadyExistsException e)
            {
                Console.WriteLine(e);
                return "The task already exists";
            }
          
            return null;
        }
    }

    class InvalidUserIdException : Exception
    {
        public InvalidUserIdException(int id) : base($"Invalid userId: {id}") {}
    }
    class TaskAlreadyExistsException : Exception
    {
        public TaskAlreadyExistsException() : base("The task already exists") {}
    }
    class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found") {}
    }
}