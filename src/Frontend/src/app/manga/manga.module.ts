import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { MangaService } from './manga.service';
import { MangaComponent } from './components/manga/manga.component';
import { HttpClientModule } from '@angular/common/http';
import { RandomMangaComponent } from './components/random-manga/random-manga.component';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { BrowserModule } from '@angular/platform-browser';

import { MangaInfoComponent } from './components/manga-info/manga-info.component';
import { ChapterModule } from '../chapter/chapter.module';
import { CommentModule } from '../comment/comment.module';

@NgModule({
  declarations: [MangaComponent, RandomMangaComponent, MangaInfoComponent],
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    ChapterModule,
    CommentModule
  ],
  providers:[
    MangaService
  ],
  exports:[
    MangaComponent, RandomMangaComponent
  ]
})
export class MangaModule { }
