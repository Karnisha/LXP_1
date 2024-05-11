using LXP.Common.ViewModels;
using LXP.Core.IServices;
using LXP.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LXP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseTopicController : BaseController
    {
        private readonly ICourseTopicServices _courseTopicServices;
        public CourseTopicController(ICourseTopicServices courseTopicServices)
        {
            _courseTopicServices = courseTopicServices;
        }

        [HttpPost("/lxp/course/topic")]
        public async Task<IActionResult> AddCourseTopic(CourseTopicViewModel courseTopic)
        {
            bool isTopicCreated = await _courseTopicServices.AddCourseTopic(courseTopic);
            if (isTopicCreated)
            {
                object topic = _courseTopicServices.GetTopicDetails(courseTopic.CourseId);
                return Ok(CreateSuccessResponse(topic));
            }
            else
            {
                return Ok(CreateFailureResponse("Topic not created", 400));
            }
            
        }

        [HttpGet("/lxp/course/{id}/topic")]
        public async Task<IActionResult> GetCourseTopicByCourseId(string id)
        {
            var CourseTopic= _courseTopicServices.GetTopicDetails(id);
            return Ok(CreateSuccessResponse(CourseTopic));
        }

        [HttpPut("/lxp/course/topic")]
        public async Task<IActionResult> UpdateCourseTopic(CourseTopicUpdateModel courseTopic)
        {
            bool updatedStatus = await _courseTopicServices.UpdateCourseTopic(courseTopic);
            if (updatedStatus)
            {
                return Ok(CreateSuccessResponse(null));
            }
            return Ok(CreateFailureResponse("", 400));
            
        }
        [HttpDelete("/lxp/course/topic/{id}")]
        public async Task<IActionResult> DeleteCourseTopic(string id)
        {
            return Ok();
        }
    }
}
