import React, { useState } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import { isAuthenticated, clearAuth } from './dataProvider';
import { LoginPage } from './screens/LoginPage';
import { GlobalMenu } from './components/GlobalMenu';
import { FlowProvider } from './flows/FlowProvider';
import { AircraftTypeList } from './screens/AircraftType/AircraftTypeList';
import { StationList } from './screens/Station/StationList';
import { AtaChapterList } from './screens/AtaChapter/AtaChapterList';
import { AircraftList } from './screens/Aircraft/AircraftList';
import { PartCatalogList } from './screens/PartCatalog/PartCatalogList';
import { SnagReportList } from './screens/SnagReport/SnagReportList';
import { SnagReportPartList } from './screens/SnagReportPart/SnagReportPartList';
import { DashboardScreen } from './screens/dashboard/DashboardScreen';
import TaskInboxScreen from './screens/tasks/TaskInboxScreen';
import UserListScreen from './admin/UserListScreen';
import RoleListScreen from './admin/RoleListScreen';

const queryClient = new QueryClient({
  defaultOptions: { queries: { retry: 1, staleTime: 0 } },
});

export const App: React.FC = () => {
  const [authenticated, setAuthenticated] = useState(isAuthenticated());


  const handleLogout = () => {
    clearAuth();
    setAuthenticated(false);
    queryClient.clear();
  };

  return (
    <QueryClientProvider client={queryClient}>
      <FlowProvider>
      <BrowserRouter>
        <Routes>
          <Route path="*" element={
            authenticated
              ? (
                <GlobalMenu>
                  <Routes>
                    <Route path="/" element={<Navigate to="/dashboard" replace />} />
          <Route path="/dashboard" element={<DashboardScreen />} />
          <Route path="/AircraftType" element={<AircraftTypeList />} />
          <Route path="/Station" element={<StationList />} />
          <Route path="/AtaChapter" element={<AtaChapterList />} />
          <Route path="/Aircraft" element={<AircraftList />} />
          <Route path="/PartCatalog" element={<PartCatalogList />} />
          <Route path="/SnagReport" element={<SnagReportList />} />
          <Route path="/SnagReportPart" element={<SnagReportPartList />} />
          <Route path="/tasks" element={<TaskInboxScreen />} />
          <Route path="/users" element={<UserListScreen />} />
          <Route path="/roles" element={<RoleListScreen />} />
                  </Routes>
                </GlobalMenu>
              )
              : <LoginPage onLogin={() => setAuthenticated(true)} />
          } />
        </Routes>
      </BrowserRouter>
        </FlowProvider>
    </QueryClientProvider>
  );
};
