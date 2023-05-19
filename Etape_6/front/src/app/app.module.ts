import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { AuthenticationComponent } from './Components/authentication/authentication.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { PageNotFoundComponent } from './Components/Others/page-not-found/page-not-found.component';
import { UnauthorizeComponent } from './Components/Others/unauthorize/unauthorize.component';
import { PostsModule } from './Components/posts/posts.module';
import { RegisterComponent } from './Components/register/register.component';
import { PostsComponent } from './Partials/posts/posts.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AuthenticationComponent,
    PageNotFoundComponent,
    UnauthorizeComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    PostsModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    HttpClient,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
