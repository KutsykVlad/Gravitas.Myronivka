namespace Gravitas.Core.Manager {

	interface IOpRoutineCoreManager {

		void StartOpRoutineTasks();
	    void StartDisplayTasks();
	    void StopTasks();
		bool IsAllFinished();
	}
}
