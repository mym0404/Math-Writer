GIST 2018 가을 학기 소프트웨어 활용 및 코딩 수업 기말 프로젝트

20165064
전기전자컴퓨터전공 문명주

----------유의 사항----------

몇 가지 패키지를 설치해야 파이썬 코드가 실행됩니다.

pip install socketIO-client3 // socket.IO를 위한 패키지
pip install opencv-python // OpenCV를 위한 패키지

----------파일 목록----------

-파이널프로젝트_문명주.pdf : 파이널 프로젝트 레포트 파일

-Game : 게임을 빌드한 것이 들어있는 폴더

-Math Writer : 유니티 게임 프로젝트 폴더
	Assets/02.Scripts 폴더 내에 C# 소스 코드가 존재

-Node : Node.js 프로젝트 폴더

-Tensorflow
	ImageCrawler.ipynb : 구글 검색을 통해 각기 다른 키워드 20개로 이미지 1000장을 다운로드 받는 크롤러 프로그램
	Image2csv.ipynb : 이미지 1000장을 OpenCV 라이브러리를 사용하여 전처리 해주고 csv파일로 저장하는 프로그램
	mnist-non_mnist.csv : MNIST의 테스트 데이터 1000개와 Non-MNIST 데이터 850개 가량의 데이터 셋
	mnist-non_mnist_test.csv : Pre-Classification 모델과 MNIST 모델의 테스트에 쓰이는 데이터
	Model+Socket.ipynb : Pre-Classification 모델, MNIST 모델, 소켓 소스 코드
	non-mnist : 다운로드 받은 1000개의 이미지 파일 폴더
	

----------사용법----------

1. 유니티 게임은 Game 폴더의 MathWriter.exe를 실행하면 Windows 환경에서 바로 실행 가능합니다.
2. Node 폴더내에서 Terminal을 실행시킨 뒤, npm start 키워드를 사용해서 app.js를 실행하면 됩니다.
3. Tensorflow 폴더의 Model+Socket.ipynb를 jupyter notebook 환경에서 실행시키면 됩니다.

감사합니다.