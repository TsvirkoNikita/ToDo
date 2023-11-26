export interface Task {
  id: number;
  title: string;
  description?: string;
  completed: boolean;
}

export interface CreateTaskRequest {
  title: string;
  description?: string;
}

export interface UpdateTaskRequest {
  id: number;
  title: string;
  description?: string;
  completed: boolean;
}

export interface GetTasksResponse {
  tasks: Task[];
  countOfActive: number;
}
