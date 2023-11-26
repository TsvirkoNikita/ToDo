import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  CreateTaskRequest,
  GetTasksResponse,
  Task,
  UpdateTaskRequest,
} from '../models/task.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private baseUrl = '/api/Task';

  constructor(public http: HttpClient) {}

  createTask(model: CreateTaskRequest): Observable<number> {
    return this.http.post<number>(this.baseUrl, model);
  }

  getTasks(completed?: boolean): Observable<GetTasksResponse> {
    return this.http.get<GetTasksResponse>(
      this.baseUrl + (completed !== undefined ? `?completed=${completed}` : '')
    );
  }

  getTask(id: number): Observable<Task> {
    return this.http.get<Task>(this.baseUrl + '/' + id);
  }

  updateTask(model: UpdateTaskRequest) {
    return this.http.put(this.baseUrl + '/' + model.id, model);
  }

  deleteTask(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }

  deleteAllCompletedTasks() {
    return this.http.delete(this.baseUrl + '/DeleteAllCompletedTasks');
  }
}
