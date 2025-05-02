import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './pages/Login';
import Register from './pages/Register';
import PrivateRoute from './components/PrivateRoute';
import MainLayout from './layouts/MainLayout'
import ArchitectureList from './pages/Architecture/ArchitectureList';
import DataBaseList from './pages/DataBase/DataBaseList';
import LanguageList from './pages/Language/LanguageList';
import RoleList from './pages/Role/RoleList';
import RelationActionList from './pages/RelationAction/RelationActionList';
import IndexTypeList from './pages/IndexType/IndexTypeList';
import ArchitectureEdit from './pages/Architecture/ArchitectureEdit';
import DataBaseEdit from './pages/DataBase/DataBaseEdit';
import IndexTypeEdit from './pages/IndexType/IndexTypeEdit';
import RelationActionEdit from './pages/RelationAction/RelationActionEdit';
import LanguageEdit from './pages/Language/LanguageEdit';
import OrmList from './pages/Orm/OrmList';
import OrmEdit from './pages/Orm/OrmEdit';
import PropertyList from './pages/Property/PropertyList';
import PropertyEdit from './pages/Property/PropertyEdit';
import RoleEdit from './pages/Role/RoleEdit';
import UserList from './pages/User/UserList';
import UserEdit from './pages/User/UserEdit';
import UserAdd from './pages/User/UserAdd';
import SqlTypeList from './pages/SqlType/SqlTypeList';
import SqlTypeEdit from './pages/SqlType/SqlTypeEdit';
import ProjectEdit from './pages/Project/ProjectEdit';
import ProjectList from './pages/Project/ProjectList';
import ProjectDiagram from './pages/Project/ProjectDiagram';
import GenerationModelList from './pages/GenerationModel/GenerationModelList';
import GenerationModelEdit from './pages/GenerationModel/GenerationModelEdit';
import GenerationLanguageList from './pages/GenerationLanguages/GenerationLanguageList';
import GenerationLanguageEdit from './pages/GenerationLanguages/GenerationLanguageEdit';

function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        <Route path="/generationModels" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <GenerationModelList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editGenerationModel/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <GenerationModelEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/generationLanguages" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <GenerationLanguageList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editGenerationLanguage/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <GenerationLanguageEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/architectures" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <ArchitectureList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editArchitecture/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <ArchitectureEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/databases" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <DataBaseList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editDatabase/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <DataBaseEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/languages" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <LanguageList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editLanguage/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <LanguageEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/roles" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <RoleList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editRole/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <RoleEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/relationActions" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <RelationActionList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editRelationAction/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <RelationActionEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/indexTypes" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <IndexTypeList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editIndexType/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <IndexTypeEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/orms" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <OrmList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editOrm/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <OrmEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/properties" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <PropertyList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editProperty/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <PropertyEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/users" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <UserList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editUser/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <UserEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/addUser" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <UserAdd />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/sqlTypes" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <SqlTypeList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editSqlType/:id" element={
          <PrivateRoute allowedRole={'Administrator'}>
            <MainLayout showHeader={true} showFooter={false}>
              <SqlTypeEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/" element={
          <PrivateRoute allowedRole={'User'}>
            <MainLayout showHeader={true} showFooter={false}>
              <ProjectList />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editProject/:id" element={
          <PrivateRoute allowedRole={'User'}>
            <MainLayout showHeader={true} showFooter={false}>
              <ProjectEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/addProject" element={
          <PrivateRoute allowedRole={'User'}>
            <MainLayout showHeader={true} showFooter={false}>
              <ProjectEdit />
            </MainLayout>
          </PrivateRoute>}
        />

        <Route path="/editProjectDiagram/:id" element={
          <PrivateRoute allowedRole={'User'}>
            <MainLayout showHeader={true} showFooter={false}>
              <ProjectDiagram />
            </MainLayout>
          </PrivateRoute>}
        />
      </Routes>
    </BrowserRouter>
  );
}

export default AppRouter;