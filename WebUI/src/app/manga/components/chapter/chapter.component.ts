import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IChapter } from '../../../shared/Models/IChapter';
import { MangaService } from '../../manga.service';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrls: ['./chapter.component.scss']
})
export class ChapterComponent implements OnInit {

  chapter:IChapter;

  @Input() chapterId:string;

  constructor(private readonly service:MangaService, private _Activatedroute:ActivatedRoute) { 
    this.chapterId = _Activatedroute.snapshot.paramMap.get("chapterId");
  }

  ngOnInit(): void {
    this.getChapter();
  }
  getChapter():void{
    this.service.getChapterBy(this.chapterId).subscribe
    (
      response=>{
        this.chapter = response;
      },
      error=>{
        alert(error);
      }
    );
  }
}
