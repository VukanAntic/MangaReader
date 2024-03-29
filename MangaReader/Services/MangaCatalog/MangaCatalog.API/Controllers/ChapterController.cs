﻿using MangaCatalog.Common.DTOs.Chapter;
using MangaCatalog.Common.DTOs.Page;
using MangaCatalog.Common.Entities;
using MangaCatalog.Common.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MangaCatalog.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChapterController : ControllerBase
    {

        private readonly IChapterRepository _repository;

        public ChapterController(IChapterRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [Route("[action]/{mangaId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChapterDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ChapterDTO>>> GetAllChaptersForMangaId(string mangaId)
        {
            var chapters = await _repository.GetChaptersByMangaId(mangaId);
            return Ok(chapters);

        }


        [Route("[action]/{chapterId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PageDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PageDTO>>> GetPagesForChapterId(string chapterId)
        {
            var pages = await _repository.GetPagesForChapterId(chapterId);
            return Ok(pages);

        }

        [Route("[action]/{chapterId}")]
        [HttpGet]
        [ProducesResponseType(typeof(ChapterDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<ChapterDTO>> GetChapterById(string chapterId)
        {
            var chapter = await _repository.GetChapterById(chapterId);
            return Ok(chapter);

        }

        [Route("[action]/{chapterId}")]
        [HttpGet]
        [ProducesResponseType(typeof(AdjacentChapterDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AdjacentChapterDTO>> GetAdjacentChapters(string chapterId)
        {
            var chapter = await _repository.GetAdjacentChapters(chapterId);
            return Ok(chapter);

        }



    }
}
