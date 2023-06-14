import { adjacentChapter } from './../manga-product/domain/models/adjacentChapter.model';
import { ActivatedRoute } from '@angular/router';
import { ChapterService } from './domain/infrastructure/chapter.service';
import { Component, OnInit } from '@angular/core';
import { Page } from '../manga-product/domain/models/page.model';
import { Chapter } from '../manga-product/domain/models/chapter.model';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrls: ['./chapter.component.css']
})
export class ChapterComponent implements OnInit {

  public pagesData! : [Page];
  public chapterId! : string;
  public chapterData! : Chapter;
  public adjacentChapterInfo! : adjacentChapter;

  constructor(private chapterService : ChapterService, private route : ActivatedRoute) { }

  ngOnInit(): void {

    this.route.paramMap.subscribe((params) => {
      this.chapterId = String(params.get("chapterId"));
      this.chapterService.getPages(this.chapterId).subscribe(
        (res) => {
          this.pagesData = res;
          console.log(this.pagesData)
        }, 
      ); 
      this.chapterService.getChapterInfo(this.chapterId).subscribe(
        (res) => {
          this.chapterData = res;
        }
      );
      this.chapterService.getAdjacentChapters(this.chapterId).subscribe(
        (res) => {
          this.adjacentChapterInfo = res;
          console.log(res);
        }
      );
    });


  }
  

}
