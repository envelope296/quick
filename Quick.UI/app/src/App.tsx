import { BrowserRouter, Route, Routes } from "react-router-dom";
import { HomePage } from "@/components/pages/HomePage";
import * as appContext from '@/services/app-context';
import * as userService from "@/services/user";
import { useBoolean, useNullableState } from "./hooks";
import { useEffect } from "react";
import { ErrorView, type ErrorViewProps } from "./components/common/error-view";
import { GroupCreatePage } from "@/components/pages/GroupCreatePage";
import { GroupPage } from "./components/pages/GroupPage";
import { SchedulesPage } from "./components/pages/SchedulesPage";
import { GroupJoinPage } from "./components/pages/GroupJoinPage";
import { ScheduleCreatePage } from "./components/pages/ScheduleCreatePage";

export function App() {
  const [error, {set: setError}] = useNullableState<ErrorViewProps>();
  const [isLoading, { setFalse: unsetIsLoading }] = useBoolean(true);

  useEffect(() => {
    async function initialize() {
      try {
        const webApp = appContext.getWebApp();
        const token = await userService.getUserToken(webApp.initData);
        appContext.setUserToken(token);
      }
      catch {
        setError({
          title: "Ошибка соединения",
          description: "Пожалуйста проверьте подключение к интернету и перезапустите мини-приложение"
        });
      }
      finally {
        unsetIsLoading();
      }
    }
    
    initialize();
  }, []);

  if (error != null) {
    return <ErrorView {...error}></ErrorView>
  }

  if (isLoading) {
    return <BeatLoader size={10} margin={3} color="#757575" speedMultiplier={0.8} />
  }
  
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/*" element={<HomePage />} />
        <Route path="/create-group/*" element={<GroupCreatePage />} />
        <Route path="/join-group/*" element={<GroupJoinPage />} />
        <Route path="/groups/:id" element={<GroupPage />} >
          <Route index element={<SchedulesPage />} />
          <Route path="schedules/*" element={<SchedulesPage />} />
          <Route path="create-schedule/*" element={<ScheduleCreatePage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}